import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CallFormModel } from '../../models/Call';
import { CallLoggerHttpClientService } from '../../services/call-logger-http-client.service';
import { Router } from '@angular/router';
import { faPhone, faLocationDot, faComment } from '@fortawesome/free-solid-svg-icons'

@Component({
  selector: 'app-log-call',
  templateUrl: './log-call.component.html',
  styleUrl: './log-call.component.scss'
})
export class LogCallComponent {

  form : FormGroup;
  isFormValid: boolean = true;
  isCallerNameValid: boolean = true;
  isDescriptionValid: boolean = true;
  isAddressValid: boolean = true;
  attemptedSubmit: boolean = false;

  faPhone = faPhone;
  faLocationDot = faLocationDot;
  faComment = faComment;

  constructor(formBuilder : FormBuilder, private http : CallLoggerHttpClientService, private router : Router) {

    let callerNameControl : FormControl = formBuilder.control('', [
      Validators.required
    ]);

    let addressControl : FormControl = formBuilder.control('', [
      Validators.required
    ]);

    let descriptionControl : FormControl = formBuilder.control('', [
      Validators.required
    ]);

    this.form = formBuilder.group({
      callerNameControl,
      addressControl,
      descriptionControl
    });
  }

  submitCall() {
    this.isFormValid = this.form.valid;
    this.isCallerNameValid = this.form.controls['callerNameControl'].valid;
    this.isAddressValid = this.form.controls['addressControl'].valid;
    this.isDescriptionValid = this.form.controls['descriptionControl'].valid;
    
    this.attemptedSubmit = true;

    if (!this.isFormValid) {
      return;
    }

    let model = new CallFormModel(
      this.form.controls['callerNameControl'].value, 
      this.form.controls['addressControl'].value, 
      this.form.controls['descriptionControl'].value
    );

    this.http.logCall(model)
        .subscribe({
          next: x => {
            this.router.navigate(['..']);
          },
          error: err => {  
            console.log(err);
          }
        });
  }

  clearErrors(controlName : 'callerNameControl' | 'addressControl' | 'descriptionControl') {

    switch (controlName) {
      case 'callerNameControl':
        this.isCallerNameValid = this.form.controls[controlName].valid;
        break;
      case 'addressControl':
        this.isAddressValid = this.form.controls[controlName].valid;
        break;
      case 'descriptionControl':
        this.isDescriptionValid = this.form.controls[controlName].valid;
        break;
    }

    this.isFormValid = this.form.valid;
  }
}
