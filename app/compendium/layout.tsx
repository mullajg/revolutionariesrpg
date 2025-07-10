import { Listbox, ListboxItem } from "@heroui/listbox";


export default function CreateNewCharacterLayout() {
  return (
    <section className="flex flex-col items-center justify-center gap-4 py-8 md:py-10">
      <div className="inline-block max-w-lg text-center justify-center">
              <Listbox aria-label="Dynamic Actions">
                  {(item) => (
                      <ListboxItem
                          key="Test"
                      >
                          Test
                      </ListboxItem>
                  )}
              </Listbox>
      </div>
    </section>
  );
}
