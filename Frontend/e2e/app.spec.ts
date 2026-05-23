import { expect, test } from '@playwright/test';

test.describe('Application shell', () => {
  test('should display the main header', async ({ page }) => {
    await page.goto('/');

    await expect(page.getByRole('heading', { name: 'TimeKeeper' })).toBeVisible();
    await expect(page.getByRole('img', { name: 'logo' })).toBeVisible();
    await expect(page.getByText('FirstName LastName')).toBeVisible();
  });
});
