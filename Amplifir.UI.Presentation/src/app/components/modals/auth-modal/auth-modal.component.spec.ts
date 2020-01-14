/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthModalComponent } from './auth-modal.component';

describe('AuthModalComponent', () => {
  let component: AuthModalComponent;
  let fixture: ComponentFixture<AuthModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
