using OpenAI.Audio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/transcibe", async Task<IResult> () =>
{
   var openAIkey = "<get API key from OpenAI portal>";
    var audiOptions = new AudioTranscriptionOptions(){
        ResponseFormat = AudioTranscriptionFormat.Srt
    };
    var audioClient = new AudioClient("whisper-1",openAIkey);
    var response = await audioClient.TranscribeAudioAsync("transcribe.mp4",audiOptions);

    return Results.Ok(new {response.Value.Text}); 
});
app.MapPost("/translate", async Task<IResult> () =>
{
   var openAIkey = "<get API key from OpenAI portal>";
    var audiOptions = new AudioTranslationOptions(){
        ResponseFormat = AudioTranslationFormat.Srt
    };
    var audioClient = new AudioClient("whisper-1",openAIkey);
    var response = await audioClient.TranslateAudioAsync("latinToEnglish.mp3");

    return Results.Ok(new {response.Value.Text}); 
});

app.Run();
