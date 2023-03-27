import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QrcodePrinterComponent } from './qrcode-printer.component';

describe('QrcodePrinterComponent', () => {
  let component: QrcodePrinterComponent;
  let fixture: ComponentFixture<QrcodePrinterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QrcodePrinterComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QrcodePrinterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
