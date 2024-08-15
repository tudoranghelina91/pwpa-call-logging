import { NgModule } from '@angular/core';
import { LogCallComponent } from './log-call.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgIf } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
  declarations: [
    LogCallComponent
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    NgIf,
    FontAwesomeModule
  ]
})
export class LogCallModule {
}
