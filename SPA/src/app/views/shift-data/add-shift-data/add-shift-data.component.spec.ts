import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddShiftDataComponent } from './add-shift-data.component';

describe('AddShiftDataComponent', () => {
  let component: AddShiftDataComponent;
  let fixture: ComponentFixture<AddShiftDataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddShiftDataComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddShiftDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
