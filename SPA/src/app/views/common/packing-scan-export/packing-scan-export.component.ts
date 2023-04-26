import { PackingScanExportDTO } from '@models/Transaction/PackingScan/PackingScanExportDTO';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Config, NgxPrintElementModule, NgxPrintElementService } from 'ngx-print-element';
import { interval, take } from 'rxjs';

@Component({
  selector: 'app-packing-scan-export',
  templateUrl: './packing-scan-export.component.html',
  styleUrls: ['./packing-scan-export.component.scss'],
  standalone: true,
  imports: [CommonModule, NgxPrintElementModule],
})
export class PackingScanExportComponent implements OnInit {
  data: PackingScanExportDTO[] = [];
  isCheck: boolean ;
  constructor(public printService: NgxPrintElementService) { }

  ngOnInit(): void {
  }

  async print(dataPrint: PackingScanExportDTO[], iSCheck?: boolean) {
    this.data = dataPrint;
    this.isCheck = iSCheck;
    let time = interval(1);
    time.pipe(take(1)).subscribe(() => {
      let config: Config = <Config>{
        printMode: 'template',
        // popupProperties: 'toolbar=yes,scrollbars=yes,resizable=yes,top=0,left=0,fullscreen=yes',
      };
      // print
      this.printService.print('printTemplate', config);
      // hidden print template
      document.getElementById('printTemplate').hidden = true;
    })
  }

}
