import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TeamsProjetsComponent } from './teams-projets.component';

describe('TeamsProjetsComponent', () => {
  let component: TeamsProjetsComponent;
  let fixture: ComponentFixture<TeamsProjetsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TeamsProjetsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TeamsProjetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
