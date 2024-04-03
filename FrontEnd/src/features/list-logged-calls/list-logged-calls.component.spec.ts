import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListLoggedCallsComponent } from './list-logged-calls.component';

describe('ListLoggedCallsComponent', () => {
  let component: ListLoggedCallsComponent;
  let fixture: ComponentFixture<ListLoggedCallsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListLoggedCallsComponent]
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
