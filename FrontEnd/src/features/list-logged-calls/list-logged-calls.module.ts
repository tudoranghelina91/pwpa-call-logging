import { NgModule } from '@angular/core';
import { ListLoggedCallsComponent } from './list-logged-calls.component';
import { HttpClientModule } from '@angular/common/http';
import { NgFor, NgIf } from '@angular/common';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    ListLoggedCallsComponent
  ],
  imports: [
    HttpClientModule,
    NgFor,
    NgIf,
    RouterModule
  ]
})
export class ListLoggedCallsModule { }