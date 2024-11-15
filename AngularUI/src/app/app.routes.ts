import { Routes } from '@angular/router';
import { HomeComponent } from './core/home/home.component';
import { LoginComponent } from './core/auth/components/login/login.component';
import { RegisterComponent } from './core/auth/components/register/register.component';
import { authGuard } from './core/auth/guards/auth.guard';
import { AdminPanelComponent } from './core/admin/components/admin-panel/admin-panel.component';
import { UserManagementComponent } from './core/admin/components/user-management/user-management.component';
import { CategoriesComponent } from './core/admin/components/categories/categories.component';
import { SubcategoriesComponent } from './core/admin/components/subcategories/subcategories.component';
import { ProductsComponent } from './core/admin/components/products/products.component';
import { CataloguePageComponent } from './core/catalogue/components/catalogue-page/catalogue-page.component';
import { CatalogueComponent } from './core/catalogue/components/catalogue/catalogue.component';
import { CatalogueProductComponent } from './core/catalogue/components/catalogue-product/catalogue-product.component';
import { CartComponent } from './core/cart/components/cart/cart.component';
import { CheckoutComponent } from './core/cart/components/checkout/checkout.component';
import { AccountComponent } from './core/account/components/account/account.component';
import { ProfileComponent } from './core/account/components/profile/profile.component';
import { CheckoutConfirmComponent } from './core/cart/components/checkout-confirm/checkout-confirm.component';
import { UserOrdersComponent } from './core/account/components/user-orders/user-orders.component';
import { UserOrderDetailsComponent } from './core/account/components/user-order-details/user-order-details.component';
import { OrdersComponent } from './core/admin/components/orders/orders.component';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { 
        path: 'admin', 
        component: AdminPanelComponent, 
        canActivate: [authGuard], 
        data: { role: 'Admin' }, 
        children: [
            { path: 'users', component: UserManagementComponent },
            { path: 'categories', component: CategoriesComponent },
            { path: 'subcategories', component: SubcategoriesComponent },
            { path: 'products', component: ProductsComponent },
            { path: 'orders', component: OrdersComponent },
        ],
    },
    { 
        path: 'catalogue', 
        component: CataloguePageComponent,
        data: { breadcrumb: { icon: 'pi pi-home' } },
        children: [
            { path: ':subcategoryName', component: CatalogueComponent },
            { path: ':subcategoryName/product/:productId', component: CatalogueProductComponent }
        ]
    },
    { path: 'cart', component: CartComponent },
    { path: 'checkout', component: CheckoutComponent },
    { path: 'checkout-confirm', component: CheckoutConfirmComponent },
    { 
        path: 'account', 
        component: AccountComponent,
        children: [
            { path: 'profile', component: ProfileComponent },
            { path: 'order-history', component: UserOrdersComponent },
            { path: 'order-details/:orderId', component: UserOrderDetailsComponent }
        ]
    }
];