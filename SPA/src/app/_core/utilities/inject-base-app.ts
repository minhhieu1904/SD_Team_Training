import { DestroyService } from '@services/destroy.service';
import { NgSnotifyService } from '@services/ng-snotify.service';
import { inject } from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { FunctionUtility } from "@utilities/function-utility";
import { NgxSpinnerService } from "ngx-spinner";
export abstract class InjectBase {
    protected router = inject(Router);
    protected translateService = inject(TranslateService);
    protected route = inject(ActivatedRoute);
    protected spinnerService = inject(NgxSpinnerService);
    protected snotifyService = inject(NgSnotifyService);
    protected destroyService = inject(DestroyService);
    protected functionUtility = inject(FunctionUtility);
}
