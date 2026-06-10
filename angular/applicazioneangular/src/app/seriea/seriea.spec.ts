import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Seriea } from './seriea';

describe('Seriea', () => {
  let component: Seriea;
  let fixture: ComponentFixture<Seriea>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Seriea],
    }).compileComponents();

    fixture = TestBed.createComponent(Seriea);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
