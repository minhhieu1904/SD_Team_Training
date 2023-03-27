import { PackingScanExportDto } from './../../../_core/models/transaction/packing-scan';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Config, NgxPrintElementModule, NgxPrintElementService } from 'ngx-print-element';

import { config, interval, take } from 'rxjs';

@Component({
  selector: 'app-packing-scan-export',
  templateUrl: './packing-scan-export.component.html',
  styleUrls: ['./packing-scan-export.component.scss'],
  standalone: true,
  imports: [CommonModule, NgxPrintElementModule]
})
export class PackingScanExportComponent implements OnInit {
  data: PackingScanExportDto[] = [];
  isCheck: boolean;

  constructor(public printService: NgxPrintElementService) { }

  ngOnInit(): void {
  }

  async print(dataPrint: PackingScanExportDto[], isCheck?: boolean) {
    this.data = dataPrint;
    this.isCheck = isCheck;
    let time = interval(1);
    time.pipe(take(1)).subscribe(() => {
      let config: Config = <Config>{
        printMode: 'template',
      };
      // print
      this.printService.print('printTemplate', config);
      // hidden print template
      document.getElementById('printTemplate').hidden = true;
    })
  }
}
