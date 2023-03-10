import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditWarehouseDataComponent } from './edit-warehouse-data.component';

describe('EditWarehouseDataComponent', () => {
  let component: EditWarehouseDataComponent;
  let fixture: ComponentFixture<EditWarehouseDataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditWarehouseDataComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditWarehouseDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
