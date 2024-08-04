import { AfterViewInit, Component } from '@angular/core';
import { Call } from '../../models/Call';
import { CallLoggerHttpClientService } from '../../services/call-logger-http-client.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-list-logged-calls',
  templateUrl: './list-logged-calls.component.html',
  styleUrl: './list-logged-calls.component.scss'
})
export class ListLoggedCallsComponent implements AfterViewInit {

  constructor(private service : CallLoggerHttpClientService, private router : Router) { }
  public calls : Call[] = [];
  canConnect = true;

  gotoLogCall() {
    this.router.navigateByUrl('calls/log');
  }

  ngAfterViewInit(): void {
    this.service.getLoggedCalls().subscribe({
      next: x => {
        this.calls = x;
      },
      error: e => {
        this.canConnect = false;
      }
    });
  }
}
