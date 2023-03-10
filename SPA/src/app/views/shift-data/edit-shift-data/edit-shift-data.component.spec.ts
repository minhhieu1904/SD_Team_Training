import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditShiftDataComponent } from './edit-shift-data.component';

describe('EditShiftDataComponent', () => {
  let component: EditShiftDataComponent;
  let fixture: ComponentFixture<EditShiftDataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditShiftDataComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditShiftDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
