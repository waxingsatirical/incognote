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

  hubConnection: HubConnection;
  messageReceived = new EventEmitter<IMessage>();
  stateChangeReceived = new EventEmitter<IStateChange>();
  connectionEstablished = new EventEmitter<boolean>();

  initialiseConnection() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.location.path() + Consts.SignalRPath)
      .build();

    this.hubConnection.on(Consts.MessageReceivedString,
      (msg: IMessage) => this.messageReceived.emit(msg));

    this.hubConnection.on(Consts.StatePostString,
      (stateChange: IStateChange) => this.stateChangeReceived.emit(stateChange));


    this.hubConnection.start()
      .then(() => this.connectionEstablished.emit(true));
    //TODO: log connection established
  }

  sendMessage(msg: IMessage) {
    this.hubConnection.invoke(Consts.SendMesssageString, msg);
  }
  joinRoom() {
    this.hubConnection.invoke(Consts.JoinString);
  }
}
