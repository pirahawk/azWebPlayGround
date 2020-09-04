import * as signalR from "@microsoft/signalr";
import { MySignalrConnection } from './my-signalr-connection';

export class SignalrFactory {

    public createConnection(): MySignalrConnection{
        let connection:signalR.HubConnection = new signalR.HubConnectionBuilder()
        .withAutomaticReconnect()
        .withUrl('/hub/mymessage')
        .build();
        
        let myConnection = new MySignalrConnection(connection);
        return myConnection;
    }
}
