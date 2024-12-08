# PDF Generator in .NET

This project is a Proof of Concept (POC) for a PDF generation tool built using .NET. The tool receives a title, author, observations, and an API key via request parameters, and generates a PDF document using LaTeX.

## What I Learned from This Project

- **Minimal APIs with .NET**: Gained experience in creating simple APIs using .NET.
- **Binary Processing Execution**: Learned how to execute and manage binary processing, such as running `pdflatex`.
- **Consuming OpenAI API**: Gained experience integrating and consuming the OpenAI API.
- **File System Management with .NET**: Practiced managing file systems using .NET, including reading, writing, and organizing temporary files.

## Features

- Generates a PDF document with customizable title and author following observations.
- Requires an API key to be passed in the request.
- Uses LaTeX to generate the PDF.

## Prerequisites

Before running the project, ensure you have the following installed:

- .NET SDK (version 6 or higher)
- LaTeX (e.g., TeX Live, MiKTeX) installed on your system to generate PDFs.
  - **Make sure `pdflatex` is available** on your PATH. 
  - You can install TeX Live or MiKTeX from their respective websites:
    - [TeX Live](https://www.tug.org/texlive/)
    - [MiKTeX](https://miktex.org/)

- A code editor like Visual Studio or Visual Studio Code (with C# support)
- NuGet packages required for PDF generation (e.g., `PdfSharpCore` for handling PDF operations)

## Setup

1. **Clone the Repository**

   Clone this repository to your local machine using the following command:

   ```bash
   git clone <repository_url>
   ```

2. **Install Dependencies**

    Navigate to the project folder and restore the NuGet packages:
    ```bash
    cd <project_folder>
    dotnet restore
    ```

3. **Install LaTeX**

    Make sure LaTeX is properly installed and pdflatex is available in your system’s PATH.

    On Linux, you can install TeX Live via your package manager:
    ```bash
    sudo apt install texlive
    ```

    On Windows, you can install MiKTeX or TeX Live and ensure the installation path is added to your environment variables.

4. **Set Up Environment Variable for pdflatex**

    To use LaTeX via pdflatex, ensure the path to the executable is set in the system environment variables.

    On Windows:

    Go to System Properties > Advanced > Environment Variables.
    Add a new system variable PATH with the path to the LaTeX binary folder (e.g., C:\texlive\2023\bin\win32 for TeX Live).
    On Linux/macOS:

    Add the LaTeX binary path to your shell configuration file (e.g., .bashrc or .zshrc):
    ```bash
    export PATH=$PATH:/path/to/latex/bin
    ```
5. **Run the Application**

    You can run the application using the following command:
    ```bash
    dotnet run
    ```
    This will start the application, which will then listen for requests to generate PDFs.

## API Usage
The PDF generator accepts a POST request with the following JSON payload:
```json
    {
        "title": "Your Title",
        "author": "Author Name",
        "observations": "Some observations",
        "apiKey": "your_api_key"
    }
```

### Request Example:
```bash
curl -X POST http://localhost:5000/generate-pdf \
     -H "Content-Type: application/json" \
     -d '{"title": "Data Strucutre and Algorithms", "author": "John Doe", "observations": "Generate only title screen and introduction", "apiKey": "your_api_key"}'
```

The response will return the generated PDF document.

## Disclaimer
This is a Proof of Concept (POC) project and may not be suitable for production environments.