$msg = Read-Host "Enter commit message"
git add .
git commit -m "$msg"
git push