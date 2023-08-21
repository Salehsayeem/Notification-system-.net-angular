import { Component } from '@angular/core';
import { WebsocketService } from './websocket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'notify-FE';
  constructor(private websocketService: WebsocketService) {
    this.requestNotificationPermission();
  }
  requestNotificationPermission() {
    if ('Notification' in window) {
      Notification.requestPermission();
    }
  }
}
