import { NgSnotifyService } from '../../../../_core/services/ng-snotify.service';
import { StandardPackingQuantitySettingService } from './../../../../_core/services/standard-packing-quantity-setting.service';
import { MS_Package } from './../../../../_core/models/mS_Package_DTO';
import { Router } from '@angular/router';
import { OperationResult } from './../../../../_core/utilities/operation-result';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {
  model: MS_Package = {} as MS_Package;
  constructor(
    private service: StandardPackingQuantitySettingService,
    private route: Router,
    private snotify: NgSnotifyService
  ) {}

  ngOnInit(): void {}

  saveChange() {
    this.snotify.confirm('Ban co muon them moi khong ?', 'Them moi', () => {
      this.service.addNew(this.model).subscribe({
        next: (res: OperationResult) => {
          if (res.isSuccess) {
            this.back();
            //Title la cai nam tren, con body la o duoi
            this.snotify.success('Add success', 'Success');
          } else {
            this.snotify.error('Add error', 'Error');
          }
        },
        error: () => {
          this.snotify.error('Add error', 'Error');
        },
        complete: () => {},
      });
    });
  }

  back() {
    this.route.navigate(['maintain/standard-parking-quantity-setting']);
  }
}
