namespace YachtingBot;

public class KeyboardLayoutBuilder
{

    public IEnumerable<IEnumerable<string>> ForVariants(IReadOnlyList<string> variants)
    {
        for (var i = 0; i < variants.Count; i++)
        {
            var variant = variants[i];
            if (i == variants.Count - 1)
            {
                yield return [variant];
                continue;
            }

            var next = variants[i + 1];
            if (variant.Length >= 32 || next.Length >= 32)
            {
                yield return [variant];
                continue;
            }

            yield return [variant, next];
            i++;
        }
    }
}