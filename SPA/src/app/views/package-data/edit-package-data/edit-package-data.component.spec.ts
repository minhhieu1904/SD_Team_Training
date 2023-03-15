import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditPackageDataComponent } from './edit-package-data.component';

describe('EditPackageDataComponent', () => {
  let component: EditPackageDataComponent;
  let fixture: ComponentFixture<EditPackageDataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditPackageDataComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditPackageDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
