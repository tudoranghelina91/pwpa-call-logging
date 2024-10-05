import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListLoggedCallsComponent } from './list-logged-calls.component';
import { ListLoggedCallsModule } from './list-logged-calls.module';
import { CallLoggerHttpClientService } from '../../services/call-logger-http-client.service';

describe('ListLoggedCallsComponent', () => {
  let component: ListLoggedCallsComponent;
  let fixture: ComponentFixture<ListLoggedCallsComponent>;

  beforeEach(async () => {
    let callLoggerServiceSpy = jasmine.createSpyObj<CallLoggerHttpClientService>(['getLoggedCalls'])

    await TestBed.configureTestingModule({
      imports: [ListLoggedCallsModule],
      providers: [{provide: CallLoggerHttpClientService, useValue: callLoggerServiceSpy}]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListLoggedCallsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
