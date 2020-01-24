/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

import { TestBed } from '@angular/core/testing';

import { ShoutsService } from './shouts.service';

describe('ShoutsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ShoutsService = TestBed.get(ShoutsService);
    expect(service).toBeTruthy();
  });
});
