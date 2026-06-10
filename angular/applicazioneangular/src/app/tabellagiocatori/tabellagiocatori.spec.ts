import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Tabellagiocatori } from './tabellagiocatori';

describe('Tabellagiocatori', () => {
  let component: Tabellagiocatori;
  let fixture: ComponentFixture<Tabellagiocatori>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Tabellagiocatori],
    }).compileComponents();

    fixture = TestBed.createComponent(Tabellagiocatori);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
