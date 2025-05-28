# Workflow Structure: Application vs. Infrastructure Deployment

As of [DATE], the deployment process for the Skill Management application is split into two separate GitHub Actions workflows:

## 1. Application Deployment (`azure-deploy.yml`)
- **Location:** `.github/workflows/azure-deploy.yml`
- **Purpose:** Builds, tests, and deploys the backend API and frontend app to Azure App Service.
- **Triggers:**
  - On push to `main` for changes in `backend/**`, `frontend/**`, or the workflow file itself
  - On pull requests to `main` (for test/build only)
  - On manual dispatch (with environment selection)
- **Stages:**
  1. Test backend and frontend
  2. Build and deploy backend API
  3. Build and deploy frontend
  4. Post deployment notification
- **Note:** This workflow no longer deploys Azure infrastructure (Bicep).

## 2. Infrastructure Deployment (`infra-deploy.yml`)
- **Location:** `.github/workflows/infra-deploy.yml`
- **Purpose:** Deploys or updates Azure infrastructure using Bicep templates and environment-specific parameters.
- **Triggers:**
  - On push to `main` for changes in `deploy/**` or the workflow file itself
  - On manual dispatch (with environment selection)
- **Stages:**
  1. Deploy Azure resources using Bicep

## Rationale
- **Separation of Concerns:** Infrastructure and application deployments are managed independently, reducing risk and improving clarity.
- **Faster Application Deployments:** Application changes do not trigger unnecessary infrastructure deployments.
- **Easier Maintenance:** Each workflow is focused and easier to troubleshoot.

## How to Use
- **Update infrastructure:** Push changes to `deploy/**` or run the `Infra Deployment` workflow manually.
- **Deploy application:** Push changes to `backend/**`, `frontend/**`, or run the `Azure Deployment` workflow manually.

---

For more details, see the main deployment documentation in `docs/github-actions-deployment.md`.
