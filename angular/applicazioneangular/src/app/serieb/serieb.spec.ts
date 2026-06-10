import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Serieb } from './serieb';

describe('Serieb', () => {
  let component: Serieb;
  let fixture: ComponentFixture<Serieb>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Serieb],
    }).compileComponents();

    fixture = TestBed.createComponent(Serieb);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
