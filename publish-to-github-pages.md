# Steps to Publish a Site to GitHub Pages

1. **Prepare Your Repository**:
   - Ensure all your changes are committed and pushed to the `main` branch.

2. **Create or Update the `gh-pages` Branch**:
   - If the `gh-pages` branch does not exist, create it by running:
     ```powershell
     git checkout --orphan gh-pages
     git reset --hard
     git add .
     git commit -m "Deploy to GitHub Pages"
     git push -u origin gh-pages --force
     ```
   - If the `gh-pages` branch already exists, switch to it and update it:
     ```powershell
     git checkout gh-pages
     git merge main --allow-unrelated-histories
     git push origin gh-pages
     ```

3. **Enable GitHub Pages**:
   - Go to your repository on GitHub.
   - Navigate to **Settings > Pages**.
   - Under the "Source" section, select the `gh-pages` branch.
   - Click **Save**.

4. **Verify the Deployment**:
   - Wait a few minutes for GitHub Pages to process the deployment.
   - Visit the URL provided in the GitHub Pages settings to view your site.
