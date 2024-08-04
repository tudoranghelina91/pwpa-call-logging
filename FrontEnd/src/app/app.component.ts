import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ListLoggedCallsModule } from '../features/list-logged-calls/list-logged-calls.module';
import { LogCallModule } from '../features/log-call/log-call.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, ListLoggedCallsModule, LogCallModule, NgbModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'pwpa-call-logging-frontend';
}
