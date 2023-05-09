/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PackingScanExportComponent } from './packing-scan-export.component';

describe('PackingScanExportComponent', () => {
  let component: PackingScanExportComponent;
  let fixture: ComponentFixture<PackingScanExportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PackingScanExportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PackingScanExportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
