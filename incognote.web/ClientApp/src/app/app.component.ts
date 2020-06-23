import { Component, NgZone } from '@angular/core';
import { IMessage } from './models/server/incognote/dal/Models/IMessage';
import { Message } from './models/Message'
import { ChatService } from './services/chat.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  text = '';
  uniqueID: string = new Date().getTime().toString();
  messages = new Array<IMessage>();
  constructor(
    private chatService: ChatService,
    private _ngZone: NgZone
  ) {
    this.subscribeToEvents();
  }
  sendMessage(): void {    
    const msg = new Message();
    msg.clientUniqueId = this.uniqueID;
    msg.payload = this.text;
    this.messages.push(msg);
    this.chatService.sendMessage(msg);
    this.text = '';
  }
  private subscribeToEvents(): void {

    this.chatService.messageReceived.subscribe((message: IMessage) => {
      this._ngZone.run(() => {
        if (message.clientUniqueId !== this.uniqueID) {
          this.messages.push(message);
        }
      });
    });
  }
}  
