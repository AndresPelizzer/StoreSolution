import { TestBed } from '@angular/core/testing';

import { Squadra } from './squadra';

describe('Squadra', () => {
  let service: Squadra;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Squadra);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
