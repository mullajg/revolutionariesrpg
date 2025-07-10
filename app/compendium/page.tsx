import { title } from "@/components/primitives";
import { Listbox, ListboxItem } from "@heroui/listbox";

export default function CompendiumPage() {
    return (
        <div>
            {/*<h1 className={title()}>Compendium</h1>*/}

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
    );
}
