import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogCallComponent } from './log-call.component';

describe('LogCallComponent', () => {
  let component: LogCallComponent;
  let fixture: ComponentFixture<LogCallComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LogCallComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LogCallComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
