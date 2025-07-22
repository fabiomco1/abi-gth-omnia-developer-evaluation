import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Fabiooliveira } from './fabiooliveira';

describe('Fabiooliveira', () => {
  let component: Fabiooliveira;
  let fixture: ComponentFixture<Fabiooliveira>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Fabiooliveira]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Fabiooliveira);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
