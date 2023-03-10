import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddWarehouseDataComponent } from './add-warehouse-data.component';

describe('AddWarehouseDataComponent', () => {
  let component: AddWarehouseDataComponent;
  let fixture: ComponentFixture<AddWarehouseDataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddWarehouseDataComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddWarehouseDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
