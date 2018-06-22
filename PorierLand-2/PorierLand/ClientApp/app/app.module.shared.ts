import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';
import { ReleaseNoteComponent } from './components/release-note/release-note.component';
import { CraftCalculatorComponent } from './components/craft-calculator/craft-calculator.component';
    import { AccordionComponent } from './components/craft-calculator/accordion.component';
import { ReminderComponent } from './components/reminder/reminder.component';
import { ActPluginComponent } from './components/act-plugin/act-plugin.component';
	import { PluginReleaseNoteComponent } from './components/plugin-release-note/plugin-release-note.component';
import { BisCalculatorComponent } from './components/bis-calculator/bis-calculator.component';
    import { EditSetComponent } from './components/bis-calculator/edit-set.component';
    import { EditMateriaComponent } from './components/bis-calculator/edit-materia.component';

import { CraftCalculatorService } from './components/craft-calculator/craft-calculator.service';
import { BisCalculatorService } from './components/bis-calculator/bis-calculator.service';
import { BisSetService } from './components/bis-calculator/bis-set.service';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        ReleaseNoteComponent,
        CraftCalculatorComponent,
            AccordionComponent,
        ReminderComponent,
        ActPluginComponent,
			PluginReleaseNoteComponent,
        BisCalculatorComponent,
            EditSetComponent,
            EditMateriaComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'release-note', component: ReleaseNoteComponent },
            { path: 'craft-calculator', component: CraftCalculatorComponent },
            { path: 'reminder', component: ReminderComponent },
            { path: 'act-plugin', component: ActPluginComponent },
			{ path: 'plugin-release-note', component: PluginReleaseNoteComponent },
            { path: 'bis-calculator/edit-set', component: EditSetComponent },
            { path: 'bis-calculator/edit-materia', component: EditMateriaComponent },
            { path: 'bis-calculator/:id', component: BisCalculatorComponent },
            { path: 'bis-calculator', component: BisCalculatorComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
		CraftCalculatorService,
        BisCalculatorService,
        BisSetService
    ]
})
export class AppModuleShared { }