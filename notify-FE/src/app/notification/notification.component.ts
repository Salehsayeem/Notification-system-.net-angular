import { Component, OnInit } from '@angular/core';
import { WebsocketService } from '../websocket.service';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit {
  notifications: string[] = [];

  constructor(private websocketService: WebsocketService) {}

  ngOnInit(): void {
    this.websocketService.getMessages().subscribe((message: any) => {
      console.log('Received message:', message);
      this.notifications.push(message); // Only push the message content
      this.showBrowserNotification(message); // Show browser notification with the message content
    });
  }

  showBrowserNotification(message: string) {
    this.websocketService.showNotification(message); // Use the showNotification function from the service
  }
}
