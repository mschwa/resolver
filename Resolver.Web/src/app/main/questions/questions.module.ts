import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import { FuseSharedModule } from '@fuse/shared.module';

import { QuestionsComponent } from './questions.component';

const routes = [
    {
        path     : 'questions',
        component: QuestionsComponent
    }
];

@NgModule({
    declarations: [
        QuestionsComponent
    ],
    imports     : [
        RouterModule.forChild(routes),

        TranslateModule,

        FuseSharedModule
    ],
    exports     : [
        QuestionsComponent
    ]
})

export class QuestionsModule
{
}
