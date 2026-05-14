import {AppComponent} from './app.component';
import {ComponentFixture, TestBed} from '@angular/core/testing';

describe('AppComponent', () => {

  let fixture: ComponentFixture<AppComponent>;
  let component: AppComponent;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AppComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;

    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should display the application title', () => {
    const element = fixture.nativeElement as HTMLElement;

    const title = element.querySelector('h1');

    expect(title).toBeTruthy();
    expect(title?.textContent).toContain('TimeKeeper');
  });

  it('should expose an accessible logo', () => {
    const element = fixture.nativeElement as HTMLElement;

    const img = element.querySelector('img.logo') as HTMLImageElement | null;

    expect(img).toBeTruthy();
    expect(img?.getAttribute('alt')).toBe('logo');
  });

  it('should display the user icon', () => {
    const element = fixture.nativeElement as HTMLElement;

    const icon = element.querySelector('mat-icon');

    expect(icon).toBeTruthy();
    expect(icon?.textContent?.trim()).toBe('person');
  });

  it('should display the user name', () => {
    const element = fixture.nativeElement as HTMLElement;

    expect(element.textContent).toContain('FirstName LastName');
  });

});
