import { NgModule } from '@angular/core';
import { ListLoggedCallsComponent } from './list-logged-calls.component';
import { HttpClientModule } from '@angular/common/http';
import { NgFor, NgIf } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { CallLoggerHttpClientService } from '../../services/call-logger-http-client.service';

@NgModule({
  declarations: [
    ListLoggedCallsComponent
  ],
  imports: [
    HttpClientModule,
    NgFor,
    NgIf,
    RouterModule,
    FontAwesomeModule
  ],
  providers: [
    CallLoggerHttpClientService
  ]
})
export class ListLoggedCallsModule { }