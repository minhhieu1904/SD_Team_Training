import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PackingScanExportComponent } from './packing-scan-export.component';

describe('PackingScanExportComponent', () => {
  let component: PackingScanExportComponent;
  let fixture: ComponentFixture<PackingScanExportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PackingScanExportComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PackingScanExportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
