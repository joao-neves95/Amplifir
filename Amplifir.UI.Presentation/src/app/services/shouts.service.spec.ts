import { TestBed } from '@angular/core/testing';

import { ShoutsService } from './shouts.service';

describe('ShoutsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ShoutsService = TestBed.get(ShoutsService);
    expect(service).toBeTruthy();
  });
});
