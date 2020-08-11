import { Injectable, EventEmitter } from '@angular/core';
import { Location } from '@angular/common';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Message } from '../models/Message';
import { IMessage } from '../models/server/incognote/dal/Models/IMessage';
import { Consts } from '../models/server/incognote/web/Consts';
import { IStateChange } from '../models/server/incognote/server/Change/IStateChange';
@Injectable({
  providedIn: 'root'
})
export class ChatService { 

  constructor(private location: Location) {
    this.initialiseConnection();
  }

  private _connectionId: string;
  hubConnection: HubConnection;
  messageReceived = new EventEmitter<IMessage>();
  stateChangeReceived = new EventEmitter<IStateChange>();
  connectionEstablished = new EventEmitter<boolean>();

  initialiseConnection() {

    var bits = this.location.path().split('/');

    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`/${bits[0]}${Consts.SignalRPath}` + (bits.length > 1 ? `?name=${bits[1]}` : ''))
      .build();

    this.hubConnection.on(Consts.MessageReceivedString,
      (msg: IMessage) => this.messageReceived.emit(msg));

    this.hubConnection.on(Consts.StatePostString,
      (stateChange: IStateChange) => this.stateChangeReceived.emit(stateChange));


    this.hubConnection.start()
      .then(() => this.hubConnection.invoke(Consts.ConnectionIdString))
      .then((id: string) => this._connectionId = id)
      .then(() => this.connectionEstablished.emit(true));
    //TODO: log connection established
  }

  connectionId(): string {
    return this._connectionId;
  }

  sendMessage(msg: string) {
    this.hubConnection.invoke(Consts.SendMesssageString, msg);
  }
  joinRoom() {
    this.hubConnection.invoke(Consts.JoinString);
  }
}
