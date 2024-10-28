import { Routes } from '@angular/router';


export const routes: Routes = [
    {
        path:'',
        redirectTo:'presentation',
        pathMatch: 'full'
    },
    {
        path:'',
        loadComponent: () => import('./master-page/master-page.component').then(a=>a.MasterPageComponent),
        children:[
            {
                path:'presentation',
                loadComponent: () => import('./shared/pages/presentation-page/presentation-page.component').then(a=>a.PresentationPageComponent),
                children:[
               

                ]
            },
            {
                path:'request',
                loadComponent: () => import('./test-requests/page/test-requests.page').then(a=>a.TestRequestsComponent),
                children:[
               

                ]
            },
            {
                path:'calendar',
                loadComponent: () => import('./tests-calendar/page/tests-calendar.component').then(a=>a.TestsCalendarComponent),
                children:[
               

                ]
            },
            {
                path:'equipments',
                loadComponent: () => import('./equipments/page/equipmentsPage/equipments.page.component').then(a=>a.EquipmentsPageComponent),
                children:[
               

                ]
            },
            {
                path:'users',
                loadComponent: () => import('./users/page/users-page.component').then(a=>a.UsersPageComponent),
                children:[
               

                ]
            },
            {
                path:'tests',
                loadComponent: () => import('./test-catalog/page/tests-page/tests-page.component').then(a=>a.TestsPageComponent),
                children:[
                

                ]
            },
            {
                path:'borrame',
                loadComponent: () => import('./borrame/borrame.component').then(a=>a.BorrameComponent),
                children:[
               

                ]
            },
            {
                path: 'employees',
                loadComponent: () => import('./employees/page/employeesPage/employees.page.component').then(a=>a.EmployeesPageComponent),

            },
            {
                path:'about',
                loadComponent: () => import('./shared/pages/about-page/about-page.component').then(a=>a.AboutPageComponent),
                children:[
               

                ]
            },
        ]

    }
];

