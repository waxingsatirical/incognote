import { IMessage } from './server/incognote/dal/Models/IMessage';

export class Message implements IMessage {
    clientUniqueId: string;
    payload: string;
}
