import { Component, NgZone } from '@angular/core';
import { IMessage } from '../models/server/incognote/dal/Models/IMessage';
import { Message } from '../models/Message'
import { ChatService } from '../services/chat.service';
import { State } from '../models/server/incognote/server/State/State';
import { IStateChange } from '../models/server/incognote/server/Change/IStateChange';


@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})  
export class MainComponent {
  text = '';
  state = new State();
  messages = new Array<IMessage>();
  constructor(
    private chatService: ChatService,
    private _ngZone: NgZone
  ) {
    this.subscribeToEvents();
  }
  sendMessage(): void {    
    this.chatService.sendMessage(this.text);
    this.text = '';
  }
  private subscribeToEvents(): void {

    this.chatService.connectionEstablished.subscribe(() => this.chatService.joinRoom()); //todo: room joined?

    this.chatService.messageReceived.subscribe((message: IMessage) => {
      this._ngZone.run(() => {
          this.messages.push(message);
      });
    });

    this.chatService.stateChangeReceived.subscribe((stateChange: IStateChange) => {
      this._ngZone.run(() => {
        var path = stateChange.path;
        path.splice(0, 0, 'state');
        var last = path.pop();
        //build tree if necessary
        var target = this;
        path.forEach(x => {
          if (target[x] == undefined) {
            target[x] = {};
          }
          target = target[x];
        });
        target[last] = stateChange.payload;
      });
    });
  }
}  
