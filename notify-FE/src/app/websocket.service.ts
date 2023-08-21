import { Injectable, OnInit } from '@angular/core';
import { webSocket } from 'rxjs/webSocket';

@Injectable({
  providedIn: 'root'
})
export class WebsocketService{

  private socket$ = webSocket('wss://localhost:7296/notify'); // Replace with your WebSocket URL
  constructor() {

    this.socket$.subscribe(
      message => {
        console.log('Received message:', message);
      },
      error => console.error('WebSocket error:', error),
      () => console.log('WebSocket connection closed')
    );

    this.socket$.next(JSON.stringify({ type: 'test', message: 'Hello WebSocket' }));
  }

  getMessages() {
    var data = this.socket$.asObservable();
    return data;
  }
  showNotification(message: string) {
    if (Notification.permission === 'granted') {
      new Notification('Notification', { body: message });
    } else if (Notification.permission !== 'denied') {
      Notification.requestPermission().then(permission => {
        if (permission === 'granted') {
          new Notification('Notification', { body: message });
        }
      });
    }
  }
}
