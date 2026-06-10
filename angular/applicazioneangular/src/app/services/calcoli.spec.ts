import { TestBed } from '@angular/core/testing';

import { Calcoli } from './calcoli';

describe('Calcoli', () => {
  let service: Calcoli;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Calcoli);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
