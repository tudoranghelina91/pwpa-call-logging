import { Component, OnInit } from '@angular/core';
import { Call } from '../../models/Call';
import { CallLoggerHttpClientService } from '../../services/call-logger-http-client.service';
import { Router } from '@angular/router';
import { faPhone, faLocationDot, faComment } from '@fortawesome/free-solid-svg-icons'

@Component({
  selector: 'app-list-logged-calls',
  templateUrl: './list-logged-calls.component.html',
  styleUrl: './list-logged-calls.component.scss'
})
export class ListLoggedCallsComponent implements OnInit {

  constructor(private service : CallLoggerHttpClientService, private router : Router) { }
  public calls : Call[] = [];
  canConnect = true;

  faPhone = faPhone;
  faLocationDot = faLocationDot;
  faComment = faComment;

  gotoLogCall() {
    this.router.navigateByUrl('calls/log');
  }

  ngOnInit(): void {
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
