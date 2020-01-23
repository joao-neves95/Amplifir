import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShoutCardComponent } from './shout-card.component';

describe('ShoutCardComponent', () => {
  let component: ShoutCardComponent;
  let fixture: ComponentFixture<ShoutCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShoutCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShoutCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
