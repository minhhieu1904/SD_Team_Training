import { PickingScanRoutingModule } from './picking-scan/picking-scan-routing.module';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'search-for-order-data',
    loadChildren: () => import('./search-for-order-data/search-for-order-data.module').then(m => m.SearchForOrderDataModule)
  },
  {
    path: 'packing-scan',
    loadChildren: () => import('./packing-scan/packing-scan.module').then(m => m.PackingScanModule)
  },
  {
    path: 'reprint-packing-scan',
    loadChildren: () => import('./reprint-packing-scan/reprint-packing-scan.module').then(m => m.ReprintPackingScanModule)
  },
  {
    path: 'picking-scan',
    loadChildren: () => import('./picking-scan/picking-scan.module').then(m => m.PickingScanModule)
  },
  {
    path: 'warehouse-scan',
    loadChildren: () => import('./warehouse-scan/warehouse-scan.module').then(m => m.WarehouseScanModule)
  },
  {
    path: 'warehouse-out-scan',
    loadChildren: () => import('./warehouse-out-scan/warehouse-out-scan.module').then(m => m.WarehouseOutScanModule)
  },
  {
    path: 'reprint-sticker',
    loadChildren: () => import('./reprint-sticker/reprint-sticker.module').then(m => m.ReprintStickerModule)
  },
  {
    path: 'cancel-sticker',
    loadChildren: () => import('./cancel-sticker/cancel-sticker.module').then(m => m.CancelStickerModule)
  },
  {
    path: 'reprint-warehouse-scan',
    loadChildren: () => import('./reprint-warehouse-scan/reprint-warehouse-scan.module').then(m => m.ReprintWarehouseScanModule)
  },
  {
    path: 'order-data-status-adjust',
    loadChildren: () => import('./order-data-status-adjust/order-data-status-adjust.module').then(m => m.OrderDataStatusAdjustModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TransactionRoutingModule { }
