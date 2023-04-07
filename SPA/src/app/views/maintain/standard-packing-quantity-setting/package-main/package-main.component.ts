import { Component, OnInit } from '@angular/core';
import { MS_Package } from '@models/maintain/mspackage';
import { InjectBase } from '@utilities/inject-base-app';
import { IconButton } from './../../../../_core/constants/common.constants';
import {StandardPackingQuantitySettingService } from './../../../../_core/services/standard-packing-quantity-setting.service'

@Component({
  selector: 'app-package-main',
  templateUrl: './package-main.component.html',
  styleUrls: ['./package-main.component.scss'],
})
export class PackageMainComponent extends InjectBase implements OnInit {
  icon = IconButton;
  data: MS_Package[] = [];

  param: MS_Package = <MS_Package>{
    manuf: 'N',
    packageNo: '',
    packageQty: 0,
  };
  constructor(private service: StandardPackingQuantitySettingService) {
    super();
  }

  ngOnInit() {}

  addNew() {
    throw new Error('Method not implemented.');
  }
  clear() {
    throw new Error('Method not implemented.');
  }
  search() {
    throw new Error('Method not implemented.');
  }
}
