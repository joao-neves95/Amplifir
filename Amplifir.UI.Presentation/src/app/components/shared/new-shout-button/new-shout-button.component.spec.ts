import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewShoutButtonComponent } from './new-shout-button.component';

describe('NewShoutButtonComponent', () => {
  let component: NewShoutButtonComponent;
  let fixture: ComponentFixture<NewShoutButtonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewShoutButtonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewShoutButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
