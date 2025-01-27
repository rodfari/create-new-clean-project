# Making `create-new-project.sh` Accessible from Any Folder on macOS

To make your `create-new-project.sh` script accessible from any folder on your Mac, follow these steps:

1. **Move the script to a directory in your `PATH`**:
   - Move your script to `/usr/local/bin`, which is a common directory for user-installed binaries.

   ```bash
   sudo mv create-new-project.sh /usr/local/bin/create-new-project

2. **Make the script executable:**

    - Ensure the script has executable permissions

```bash
sudo chmod +x /usr/local/bin/create-new-project
```

3. **Verify the script is in your PATH:**

    - Check if bin is in your PATH by running:

```bash
echo $PATH
```

- If bin is not in your `PATH`, add it by editing your shell profile file (e.g., `~/.zshrc` for Zsh or `~/.bash_profile` for Bash) and adding the following line:

```bash
export PATH="/usr/local/bin:$PATH"
````

- After editing the file, reload your shell configuration:

```bash
source ~/.zshrc  # or source ~/.bash_profile
```

Now, you should be able to run `create-new-project` from any directory in your terminal.